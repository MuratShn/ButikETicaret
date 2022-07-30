﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikedProductController : ControllerBase
    {
        private readonly ILikedProductManager _productLikedService;
        public LikedProductController(ILikedProductManager productLikedService_)
        {
            this._productLikedService = productLikedService_;
        }

        [HttpPost("addFavorites")]
        public IActionResult AddFavorites(LikedProduct entity)
        {
            var result = _productLikedService.AddFavorite(entity);
            return Ok(result);
        }
        [HttpGet("getFavorites")]
        public IActionResult GetFavorites()
        {
            var userId = User.Identities.First().Name;
            var result = _productLikedService.GetFavoriteProducts(int.Parse(userId));
            return Ok(result);
        }
    }
}
