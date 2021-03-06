﻿using Microsoft.Owin;

namespace Snow.Web.Test.Extensions.OwinRequestExtension
{
    public class BaseTest
    {
        protected IOwinRequest BuildIOwinRequest(string header, string value)
        {
            var owinRequest = new OwinRequest();

            if (value !=null)
            {
               owinRequest.Headers.Add(header, new[] { value });
            }

            return owinRequest;
        }
    }
}
