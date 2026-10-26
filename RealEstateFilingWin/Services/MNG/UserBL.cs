using RealEstateFiling.Services;

using RealEstateFilingWin.Models.MNG;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Services.MNG
{
    public class UserBL:RepoFullBase<User>
    {
        public UserBL(RealEstateContext realEstateContext) :base(realEstateContext)
        {
            _Contexct = realEstateContext;
        }
        public UserBL():base()
        {
            _Contexct = new RealEstateContext();
        }

        public OperationResult SaveUser(User user)
        {
            OperationResult op = new OperationResult();
            try
            {

                _Contexct.Users_List.Add(user);
                _Contexct.SaveChanges();
                op.Status=OperationResult.OpreationResultStatus.Success;
            }
            catch (Exception ex)
            {
                op.Status = OperationResult.OpreationResultStatus.Error;
                op.Message = ex.Message;
                
            }
            return op;
        }


    }
}
