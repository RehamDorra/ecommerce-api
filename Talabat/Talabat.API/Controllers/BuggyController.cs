using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Not Found Request
        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(1000);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        #endregion

        #region Server Error
        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(1000);
            var result = product.ToString();  //will through null reference exception
            return Ok(result);
        }
        #endregion

        #region Bad Request
        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        #endregion

        #region Validation Error
        [HttpGet("badRequest/{id}")]
        public ActionResult GetBadRequest(int? id)
        {
            return Ok();
        }
        #endregion

        #region UnAutorized
        [HttpGet("unAuthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        } 
        #endregion

    }
}
