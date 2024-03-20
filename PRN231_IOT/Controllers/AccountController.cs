using BusinessObject.Common;
using BusinessObject.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    /// <summary>
    /// Create account 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
    {
        var result = await _accountService.Create(request);
        return StatusCode((int)result.StatusCode, result);
    }
    
    /// <summary>
    /// Search account
    /// </summary>
    /// <param name="searchBaseReq"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq searchBaseReq)
    {
        var result = await _accountService.Search(searchBaseReq.KeySearch, searchBaseReq.PagingQuery,
            searchBaseReq.OrderBy);
        return StatusCode((int)result.StatusCode, result);
    }
    
    /// <summary>
    /// Get account by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _accountService.GetById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Update account
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountRequest request)
    {
        var result = await _accountService.Update(id, request);
        return StatusCode((int)result.StatusCode, result);
    }
    
    /// <summary>
    /// Delete account
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _accountService.Delete(id);
        return StatusCode((int)result.StatusCode, result);
    }
    
    
    
}