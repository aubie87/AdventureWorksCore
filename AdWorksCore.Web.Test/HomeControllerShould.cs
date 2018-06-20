using AdWorksCore.Web.ControllersView;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AdWorksCore.Web.Test
{
    public class HomeControllerShould
    {
        [Fact]
        public void ReturnIndexViewTypeForIndex()
        {
            HomeController sut = new HomeController();

            IActionResult result = sut.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
