using API.Helper.Mapper;
using API.ViewModel.File;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTO.File;
using FileSystemLoader.Interface;
using Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.File
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : BaseController
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FilesController(IFileService fileService, IUserService userService) : base(userService, MapConfig.FileConfig())
        {
            this._fileService = fileService;
            _mapper = new Mapper(MapConfig.FileConfig());
        }

        [HttpGet]
        public ActionResult<Task<IQueryable<FileVM>>> Get()
        {
            return ExecuteWithRezalt(async () => (await _fileService.GetFilesInfo(await GetUserId())).ProjectTo<FileVM>(_mapperConfig));
        }

        [HttpGet("id")]
        public ActionResult<Task<FileVM>> Get(Guid id)
        {
            return ExecuteWithRezalt(async () =>
            {
                var userId = await GetUserId();
                return _mapper.Map<FileVM>(await _fileService.GetFileInfo(userId, id));
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Task<Guid>> Create(FileVM file)
        {
            return ExecuteWithRezalt(async () =>
            {
                if (Request.Form.Files.Count < 1)
                    throw new ArgumentNullException("Mast contain file");

                var userId = await GetUserId();
                var file = Request.Form.Files[0];
                var fileStream = new MemoryStream();
                await file.CopyToAsync(fileStream);
                var fileDto = _mapper.Map<UserFileDTO>(file);
                return _fileService.CreateFileAsync(userId, fileDto, fileStream, true);
            });
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult<Task<bool>> EditAsync(FileVM file)
        {
            return ExecuteWithRezalt(async () =>
            {
                var userId = await GetUserId();
                var fileDto = _mapper.Map<UserFileDTO>(file);
                return await _fileService.UpdateFileInfo(userId, fileDto);
            });
        }

        [HttpDelete("id")]
        public ActionResult<Task<bool>> DeleteAsync(Guid id)
        {
            return ExecuteWithRezalt(async () =>
            {
                var userId = await GetUserId();
                return await _fileService.DeleteFile(userId, id);
            });
        }
    }
}
