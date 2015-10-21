using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData;
using OData.Models;


//https://sudoman1012.wordpress.com/2014/03/01/networld-getting-started-with-odata-v4-in-asp-net-web-api/
namespace OData.Controllers
{
    public class PlayerController : ODataController
    {
        private PlayerAppContext db = new PlayerAppContext();
        //This class [enablequery] defines an attribute that can be applied to an action to enable querying using the OData query syntax.
        [EnableQuery]
        public IQueryable<Player> GetPlayer()
        {
            return db.Players;
        }

        [EnableQuery]
        public SingleResult<Player> GetPlayer([FromODataUri] int key)
        {
            return SingleResult.Create(db.Players.Where(player => player.Id == key));
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != player.Id)
            {
                return BadRequest();
            }
            db.Entry(player).State = EntityState.Modified;//?
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(key))
                {
                    return NotFound();
                }
                throw;
            }
            return Updated(player);
        }

        public async Task<IHttpActionResult> Post(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Players.Add(player);
            await db.SaveChangesAsync();
            return Created(player);
        }
    }
}
