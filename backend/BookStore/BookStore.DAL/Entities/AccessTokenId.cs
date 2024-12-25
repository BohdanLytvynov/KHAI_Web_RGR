using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class AccessTokenId
    {       
        public Guid AccessTokenGUID { get; set; }

        public DateTime ExpDate { get; set; }// The date when token Id will expire, need to clean up this records

        #region Navigation Properties

        public User User { get; set; }

        public Guid UserId { get; set; }

        #endregion


    }
}
