using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDemo.Models
{
    public record DownloadResult(string Url, string Content, bool FromCache,Exception? Error = null);
}
