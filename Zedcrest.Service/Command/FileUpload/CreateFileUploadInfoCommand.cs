using MediatR;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Command
{
    public class CreateFileUploadInfoCommand : IRequest<FileUploadInfo>
    {
        public FileUploadInfo FileUploadInfo { get; set; }
    }
}
