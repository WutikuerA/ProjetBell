using Assets.Module.DataService;
using Assets.Module.Filters;
using Assets.Module.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Assets.Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetManagementController : ControllerBase
    {
        private AssetDataContext _dbContext;
        public AssetManagementController(AssetDataContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAssets")]
        public List<Asset> GetAssets([FromQuery] AssetFilter filter)
        {
            var assetList = from assets in _dbContext.Assets select assets;            

            return assetList.ToList();
        }




    }
}
