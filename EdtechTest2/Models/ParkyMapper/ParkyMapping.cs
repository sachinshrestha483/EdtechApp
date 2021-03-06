using AutoMapper;
using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models.ParkyMapper
{
    public class ParkyMapping:Profile
    {
        public ParkyMapping()
        {
            CreateMap<Lecture,LectureDto>().ReverseMap();
            CreateMap<Content, ContentDto>().ReverseMap();

            CreateMap<Lecture, CourseLecturePDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<Coupon, CouponDto>().ReverseMap();

            CreateMap< CourseHomePageDto, Course>().ReverseMap();






            //More Models for More Mapping and remapping
        }

    }
}
