﻿using System.Web.Http;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/security")]
  public class SecurityController : ApiController
  {
    [Authorize]
    [Route("Auth")]
    public IHttpActionResult Get()
    {
      return Ok(new TestData());
    }

    [Route("NonAuth")]
    public IHttpActionResult GetNonAuth()
    {
      return Ok(new TestData());
    }
  }

  public class TestData
  {
    public TestData()
    {
      Hello = "Hello World";
    }

    public string Hello { get; set; }
  }
}