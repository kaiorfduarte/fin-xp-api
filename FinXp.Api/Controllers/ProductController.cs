using AutoMapper;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinXp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService,
    IMapper mapper) : Controller
{
    [HttpGet]
    [Route("GetProductList")]
    public async Task<ActionResult<List<ProductResponse>>> GetProductList()
    {
        var resultProductList = await productService.GetProductListAsync();

        if (resultProductList.IsSuccess)
        {
            if(resultProductList.Data.Count == 0)
            {
                return NoContent();
            }

            var response = mapper.Map<List<ProductResponse>>(resultProductList.Data);

            return Ok(response);
        }
        else
        {
            return Problem(resultProductList.Error.Message);
        }
    }

    [HttpGet]
    [Route("GetClientProductList")]
    public async Task<ActionResult<List<ProductResponse>>> GetClientProductList(int clientId)
    {
        var resultClientProductList = await productService.GetClientProductAsync(clientId);

        if (resultClientProductList.IsSuccess)
        {
            if (resultClientProductList.Data.Count == 0)
            {
                return NoContent();
            }

            var response = mapper.Map<List<ProductResponse>>(resultClientProductList.Data);

            return Ok(response);
        }
        else
        {
            return Problem(resultClientProductList.Error.Message);
        }
    }
}
