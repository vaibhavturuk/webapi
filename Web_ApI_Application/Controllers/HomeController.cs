using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_ApI_Application.Dtos;
using Web_ApI_Application.Models;

namespace Web_ApI_Application.Models
{
    public class MemberController : ApiController
    {
        private MemberContext context;
        public MemberController()
        {
            context = new MemberContext();
        }
        // GET api/values
        public IEnumerable<Member> Get()
        {
            return context.Members.ToList();

            //return new Member[]
            //{
            //    new Member
            //   {
            //    MemId = 1,
            //    MemberName = "Glenn Block",
            //    MemberEmail="genn@gmail.com",
            //    Address="Address genn"
            //   },
            //    new Member
            //    {
            //        MemId = 2,
            //        MemberName = "Dan Roth",
            //        MemberEmail="dan@gmail.com",
            //        Address="Address dan"
            //    },
            //    new Member
            //    {
            //        MemId = 3,
            //        MemberName = "Ban koth",
            //        MemberEmail="Ban@gmail.com",
            //        Address="Address ban"
            //    }
            //};
        }
        public MemberDto Get(int id)
        {
            var member = context.Members.SingleOrDefault(c => c.MemId == id);
            if (member == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Member, MemberDto>(member);

        }
        // by using AutoMapper


        //[HttpPost]
        //public MemberDto CreateMember(MemberDto memberDto)
        //{
        //    if (!ModelState.IsValid)
        //         throw new HttpResponseException(HttpStatusCode.BadRequest);
        //        //return BadRequest();
        //    var member = Mapper.Map<MemberDto, Member>(memberDto);
        //    context.Members.Add(member);
        //    context.SaveChanges();
        //    memberDto.MemId = member.MemId;
        //    return memberDto;
        //}

        //By using IHttpActionResult
        [HttpPost]
        public IHttpActionResult CreateMember(MemberDto memberDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            var member = Mapper.Map<MemberDto, Member>(memberDto);
            context.Members.Add(member);
            context.SaveChanges();
            memberDto.MemId = member.MemId;
            return Created(new Uri(Request.RequestUri + "/" + member.MemId), memberDto);
        }

        [HttpPut]
        public void UpdateMember(int id, MemberDto memberDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var memberInDb = context.Members.SingleOrDefault(c => c.MemId == id);
            if (memberInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(memberDto, memberInDb);
            //memberInDb.MemName = member.MemName;
            //memberInDb.MemEmail = member.MemEmail;
            //memberInDb.MemAddress = member.MemAddress;
            context.SaveChanges();



        }
        [HttpDelete]
        public void DeleteMember(int id)
        {
            var memberInDb = context.Members.SingleOrDefault(c => c.MemId == id);
            if (memberInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            context.Members.Remove(memberInDb);
        }
    }
}

