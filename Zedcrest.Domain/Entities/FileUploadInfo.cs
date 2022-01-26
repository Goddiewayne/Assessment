using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zedcrest.Domain.Entities
{
    public class FileUploadInfo : IEntity
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public long UserId { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}