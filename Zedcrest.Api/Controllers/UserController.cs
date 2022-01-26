using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zedcrest.Api.Models.FileUpload;
using Zedcrest.Data.RabbitQueue;
using Zedcrest.Service.EmailService;

namespace Zedcrest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        //private readonly ILogger<UserController> _logger;
        //private readonly IBus _busControl;
        private readonly EmailConfiguration _emailConfig;

        //public UserController(IMediator mediator, IMapper mapper, ILogger<UserController> logger, IBus busControl, EmailConfiguration emailConfig)
        //{
        //    _mediator = mediator;
        //    _mapper = mapper;
        //    _logger = logger;
        //    _busControl = busControl;
        //    _emailConfig = emailConfig;
        //}

        public UserController(IMediator mediator, IMapper mapper, EmailConfiguration emailConfig)
        {
            _mediator = mediator;
            _mapper = mapper;
            _emailConfig = emailConfig;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreateUser")]
        public async Task<ActionResult<CreateUserResponseDto>> Create([FromForm] CreateUserRequestDto createUserModel)
        {
            long size = createUserModel.FileUploads.Sum(f => f.Length);
            var files = createUserModel.FileUploads;
            List<FileUploadResponseDto> FileUploadResponseDtos = new();
            List<FileUploadInfo> fileUploads = new();
            try
            {
                var user = _mapper.Map<User>(createUserModel);
                user.DateCreated = DateTime.Now;
                foreach (var f in files)
                {
                    string name = f.FileName.Replace(@"\\\\", @"\\");

                    if (f.Length > 0)
                    {
                        var memoryStream = new MemoryStream();

                        try
                        {
                            await f.CopyToAsync(memoryStream);

                            // Upload check if less than 2mb!
                            if (memoryStream.Length < 2097152)
                            {
                                var file = new FileUploadInfo()
                                {
                                    FileName = Path.GetFileName(name),
                                    FileSize = memoryStream.Length,
                                    UserId = user.Id,
                                    FileContent = memoryStream.ToArray(),
                                    DateCreated = DateTime.Now
                                };

                                fileUploads.Add(file);

                                FileUploadResponseDtos.Add(new FileUploadResponseDto()
                                {
                                    Id = file.Id,
                                    Status = "OK",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "",
                                });
                            }
                            else
                            {
                                FileUploadResponseDtos.Add(new FileUploadResponseDto()
                                {
                                    Id = 0,
                                    Status = "ERROR",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "File " + f + " failed to upload"
                                });
                            }
                        }
                        finally
                        {
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                }
                user.FileUploads = fileUploads;
                user.UniqueReference = Guid.NewGuid().ToString(); //$"{DateTime.Now:ddyyMMHHmmss}";
                var response = await _mediator.Send(new CreateUserCommand
                {
                    User = user
                });
                //if (createUserModel == null)
                //{
                //    _logger.LogDebug("BadRequest - ConfigurationDto is null or empty");
                //    return BadRequest();
                //}

                //await _busControl.SendAsync(Queue.Processing, createUserModel);

                var message = new EmailMessage();
                var userEmailAddress = new EmailAddress
                {
                    Name = user.Name,
                    Address = user.EmailAddress
                };

                message.ToAddresses.Add(userEmailAddress);
                message.Subject = "Zedcrest Assessment";
                message.Content = "Kindly confirm receipt of this mail.";
                message.Attachments = files;

                var _emailService = new EmailService(_emailConfig);
                _emailService.SendEmail(message);
                return _mapper.Map<CreateUserResponseDto>(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetUserByEmailAndUniqueRef")]
        public async Task<ActionResult<FindUserResponseDto>> GetUserByEmailAndUniqueRef([FromQuery] FindUserByEmailQuery requestModel)
        {
            try
            {
                var result = await _mediator.Send(requestModel);
                return _mapper.Map<FindUserResponseDto>(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("FindUser")]
        public async Task<ActionResult<FindUserResponseDto>> Find([FromQuery(Name = "id")] long userId)
        {
            try
            {
                var response = await _mediator.Send(new FindUserQuery
                {
                    UserId = userId
                });

                return _mapper.Map<FindUserResponseDto>(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
