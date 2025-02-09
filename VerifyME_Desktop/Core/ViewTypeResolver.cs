using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public interface IViewTypeResolver
    {
        Type GetViewType();
    }
    class ViewTypeResolver : IViewTypeResolver
    {
        private readonly object _viewModel;

        public ViewTypeResolver(object viewModel)
        {
            _viewModel = viewModel;
        }

        public Type GetViewType()
        {
            string? namespaceName = Assembly.GetExecutingAssembly().GetName().Name;
            string viewName = _viewModel.GetType().Name.Replace("ViewModel", "View");
            return Type.GetType($"{namespaceName}.Views.{viewName}")
                   ?? throw new InvalidOperationException("View type not found.");
        }
    }

}
