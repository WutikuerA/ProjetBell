
using Microsoft.AspNetCore.Mvc;
using ProjetBellAPI.DataService;
using ProjetBellAPI.Filters;
using ProjetBellAPI.Models;

namespace ProjetBellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private DataContext _dbContext;
        public AssetController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAssets")]
        public List<Asset> GetAssets([FromQuery] AssetFilter filter)
        {
            var assetList = from assets in _dbContext.Assets select assets;

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
                _dbContext.Assets.Add(asset);
            }
            else
            {
                //Edition
                _dbContext.Assets.Update(asset);
            }

            bool isSaved = _dbContext.SaveChanges() > 0;

            return isSaved;
        }

        [HttpDelete("{assetId}")]
        public bool DeleteAsset(int assetId)
        {
            // Find the asset to be removed
            Asset? asset = _dbContext.Assets.Where(a => a.Id == assetId)?.FirstOrDefault();

            // Delete the asset
            if(asset != null)
            {
                var changeTrack = _dbContext.Assets.Remove(asset);
            }

            bool isSaved = _dbContext.SaveChanges() > 0;

            return isSaved;
        }

        




    }
}
