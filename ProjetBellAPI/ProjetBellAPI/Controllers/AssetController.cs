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
    public class AssetController : ControllerBase
    {
        private AssetDataContext _dbContext;
        public AssetController(AssetDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAssets")]
        public List<Asset> GetAssets([FromQuery] AssetFilter filter)
        {
            var assetList = from assets in _dbContext.Assets select assets;

            return assetList.ToList();
        }

        [HttpPost]
        public bool ModifyAsset(Asset asset)
        {
            if(asset.Id == 0)
            {
                // Creation
                var post = _dbContext.Assets.Add(asset);
            }
            else
            {
                //Edition
                var post = _dbContext.Assets.Update(asset);
            }

            bool isSaved = _dbContext.SaveChanges() > 0;

            return isSaved;
        }




    }
}
