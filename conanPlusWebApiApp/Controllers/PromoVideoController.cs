using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoVideoController : ControllerBase
    {
        private readonly ICommonRepository<PromoVideo> _promoVideoRepository;
        private readonly IWebHostEnvironment _env;  

        public PromoVideoController(ICommonRepository<PromoVideo> promoVideoRepository, IWebHostEnvironment env)
        {
            _promoVideoRepository = promoVideoRepository;
            _env = env;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PromoVideo>> GetPromoVideo()
        {
            try
            {
                var videoList = await _promoVideoRepository.GetAll();
                if (videoList == null || videoList.Count == 0)
                {
                    return NotFound("No video found.");
                }

                return Ok(videoList[0]); // Assuming there will be only one video.
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrUpdatePromoVideo([FromForm] IFormFile videoFile)
        {
            if (videoFile == null)
            {
                return BadRequest(new { message = "Video file is required." });
            }

            try
            {
                string videoFilePath = null;

                if (videoFile != null)
                {
                    var uploadFolder = Path.Combine(_env.WebRootPath, "videos");

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{videoFile.FileName}";
                    videoFilePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(videoFilePath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(fileStream);
                    }
                }

                var existingVideos = await _promoVideoRepository.GetAll();
                if (existingVideos.Count > 0)
                {
                    var existingVideo = existingVideos[0];

                    if (!string.IsNullOrEmpty(existingVideo.VideoFilePath))
                    {
                        var oldFilePath = Path.Combine(_env.WebRootPath, existingVideo.VideoFilePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    existingVideo.VideoFilePath = $"/videos/{Path.GetFileName(videoFilePath)}";
                    await _promoVideoRepository.Update(existingVideo);
                    return Ok(existingVideo);
                }
                else
                {
                    var newVideo = new PromoVideo
                    {
                        VideoFilePath = $"/videos/{Path.GetFileName(videoFilePath)}",
                        DateAdded = DateTime.UtcNow
                    };

                    var addedVideo = await _promoVideoRepository.Insert(newVideo);
                    return Ok(addedVideo);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePromoVideo()
        {
            try
            {
                var existingVideos = await _promoVideoRepository.GetAll();
                if (existingVideos.Count == 0)
                {
                    return NotFound("No video found to delete.");
                }

                var videoToDelete = existingVideos[0];

                if (!string.IsNullOrEmpty(videoToDelete.VideoFilePath))
                {
                    var filePath = Path.Combine(_env.WebRootPath, videoToDelete.VideoFilePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                await _promoVideoRepository.Delete(videoToDelete.VideoId);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
