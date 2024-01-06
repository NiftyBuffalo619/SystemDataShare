using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Forms;

namespace SystemDataShare.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        
        [HttpGet]
        [Route("api/schreenshot")]
        public HttpResponseMessage GetScreenshot()
        {
            byte[] screenshotBytes = CaptureScreenshot();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(screenshotBytes);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            return response;
        }
        [HttpGet]
        [Route("api/diskspace")]
        public IHttpActionResult GetDiskSpace()
        {
            DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory));

            var diskSpaceInfo = new
            {
                DriveLetter = driveInfo.Name,
                TotalSpace = driveInfo.TotalSize,
                FreeSpace = driveInfo.AvailableFreeSpace,
                UsedSpace = driveInfo.TotalSize - driveInfo.AvailableFreeSpace
            };

            return Ok(diskSpaceInfo);
        }

        private byte[] CaptureScreenshot()
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                screenshot.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
