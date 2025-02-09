using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerifyME_Desktop.Core;

namespace VerifyME_Desktop.ViewModels
{
    public class BBReconciliationViewModel: INotifyPCME, IViewTypeResolver
    {
        private readonly INavigationService _navigationService;
        private readonly IViewTypeResolver _resolver;
        public BBReconciliationViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            _resolver = new ViewTypeResolver(this);
        }

        public Type GetViewType()
        {
            return _resolver.GetViewType();
        }
    }
}
