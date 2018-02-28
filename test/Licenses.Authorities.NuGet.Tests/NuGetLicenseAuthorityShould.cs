using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Licenses.Core;
using NSubstitute;
using Xunit;

namespace Licenses.Authorities.NuGet.Tests
{
    public class NuGetLicenseAuthorityShould
    {
        private static readonly IList<License> s_knownLicenses = new List<License>
        {
            new License
            {
                Key = "mit",
                Name = "MIT",
                Text = @"The MIT License (MIT)
Copyright(c) 2016 lvermeulen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files(the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE."
            }
        };

        private readonly ILicenseAuthority _authority = new NuGetLicenseAuthority(s_knownLicenses);

        [Fact]
        public async Task GetLicenseAsync()
        {
            var result = await _authority.GetLicenseAsync("Flurl.Http.Xml", "1.5.0");
            Assert.Equal("MIT", result.Key, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ReturnNullWhenPackageNotFound()
        {
            var result = await _authority.GetLicenseAsync("Yup", "0.24.1");
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenLicenseReaderReturnsNull()
        {
            var stub = Substitute.For<ILicenseUrlReader>();
            stub.GetLicenseTextAsync("https://github.com/lvermeulen/Flurl.Http.Xml/blob/master/LICENSE")
                .Returns(Task.FromResult<string>(null));

            var authority = new NuGetLicenseAuthority(stub);
            var result = await authority.GetLicenseAsync("Flurl.Http.Xml", "1.5.0");
            Assert.Null(result);
        }
    }
}
