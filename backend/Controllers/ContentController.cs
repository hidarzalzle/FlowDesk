using System.Security.Claims;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var isAdmin = User.IsInRole("Admin");
        return Ok(await _contentService.GetAllAsync(isAdmin));
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var isAdmin = User.IsInRole("Admin");
        var item = await _contentService.GetByIdAsync(id, isAdmin);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] UpsertContentItemDto request)
    {
        var created = await _contentService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertContentItemDto request)
    {
        var updated = await _contentService.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _contentService.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
