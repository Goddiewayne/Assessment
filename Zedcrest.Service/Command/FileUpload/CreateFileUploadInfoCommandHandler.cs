using MediatR;
using Zedcrest.Data.Repository;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Command
{
    public class CreateFileUploadInfoCommandHandler : IRequestHandler<CreateFileUploadInfoCommand, FileUploadInfo>
    {
        private readonly IFileUploadInfoRepository _fileUploadRepository;
        public CreateFileUploadInfoCommandHandler(IFileUploadInfoRepository fileUploadRepository) => _fileUploadRepository = fileUploadRepository;
        public async Task<FileUploadInfo> Handle(CreateFileUploadInfoCommand request, CancellationToken cancellationToken)
        {
            return await _fileUploadRepository.AddAsync(request.FileUploadInfo);
        }
    }
}
