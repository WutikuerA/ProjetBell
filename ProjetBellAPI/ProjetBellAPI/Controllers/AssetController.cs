
using AssetModule.DataService;
using AssetModule.Filters;
using AssetModule.Models;
using Microsoft.AspNetCore.Mvc;


namespace ProjetBellAPI.Controllers
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
            var assetList = from assets in _dbContext.Asset select assets;

            // Apply filter search on name
            List<Asset> filteredList = new List<Asset>();
            if(filter.Name != null)
            {
                filteredList = assetList.Where(a => a.Name.Contains(filter.Name)).ToList();
                return filteredList;
            }


            return assetList.ToList();
        }

        [HttpPost]
        public bool ModifyAsset(Asset asset)
        {
            asset.ModificationDate = DateTime.Now;

            if (asset.Id == 0)
            {
                // Creation
                _dbContext.Asset.Add(asset);
            }
            else
            {
                //Edition
                _dbContext.Asset.Update(asset);
            }

            bool isSaved = _dbContext.SaveChanges() > 0;

            return isSaved;
        }

        [HttpDelete("{assetId}")]
        public bool DeleteAsset(int assetId)
        {
            // Find the asset to be removed
            Asset? asset = _dbContext.Asset.Where(a => a.Id == assetId)?.FirstOrDefault();

            // Delete the asset
            if(asset != null)
            {
                var changeTrack = _dbContext.Asset.Remove(asset);
            }

            bool isSaved = _dbContext.SaveChanges() > 0;

            return isSaved;
        }

        




    }
}
