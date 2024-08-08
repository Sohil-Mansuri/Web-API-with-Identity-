using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments.Select(c => c.ToCommentsDTo()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return Unauthorized();

            if (comment == null) return NotFound();
            comment.Username = appUser.UserName ?? string.Empty;
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return Unauthorized();

            var comment = createCommentDto.ToComment();
            comment.AppUserID = appUser.Id;
            await _commentRepository.Add(comment);
            return CreatedAtAction(nameof(GetByID), new { id = comment.ID }, comment.ToCommentsDTo());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CommentsDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.Update(id, createCommentDto);
            if (comment == null) return NotFound();
            return Ok(comment);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.Delete(id);
            if (comment == null) return NotFound();
            return NoContent();
        }
    }
}
