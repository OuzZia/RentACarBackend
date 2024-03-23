using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalDb>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalDb context = new CarRentalDb())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.Id
                             join clr in context.Colors
                             on c.ColorId equals clr.Id
                             join brnd in context.Brands
                             on c.BrandId equals brnd.Id
                             join cust in context.Customers
                             on r.CustomerId equals cust.Id
                             join us in context.Users
                             on cust.UserId equals us.Id

                             select new RentalDetailDto
                             {
                                 Id = r.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CarId = c.Id,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 BrandId = c.BrandId,
                                 ColorId = c.ColorId,
                                 ColorName = clr.ColorName,
                                 BrandName = brnd.BrandName,
                                 CompanyName = cust.CompanyName,
                                 CustomerName = us.FirstName + " " + us.LastName
                             };

                //var result = from r in context.Rentals
                //             join c in context.Cars
                //             on r.CarId equals c.Id
                //             join cu in context.Customers
                //             on r.CustomerId equals cu.Id
                //             join color in context.Colors
                //             on c.ColorId equals color.Id
                //             join br in context.Brands
                //             on c.BrandId equals br.Id
                //             join u in context.Users
                //             on r.CustomerId equals u.Id
                //             select new RentalDetailDto
                //             {
                //                 Id = r.Id,
                //                 ColorId = color.Id,
                //                 BrandId = br.Id,
                //                 ColorName = color.ColorName,
                //                 BrandName = br.BrandName,
                //                 CompanyName = cu.CompanyName,
                //                 RentDate = r.RentDate,
                //                 ReturnDate = r.ReturnDate,
                //                 DailyPrice = c.DailyPrice,
                //                 ModelYear = c.ModelYear,
                //                 Description = c.Description,
                //                 CarId = c.Id,
                //                 CustomerName = u.FirstName + " " + u.LastName,
                //             };


                //var result = from r in context.Rentals
                //             join c in context.Cars
                //             on r.CarId equals c.Id
                //             join cu in context.Customers
                //             on r.CustomerId equals cu.Id
                //             join u in context.Users
                //             on r.CustomerId equals u.Id

                //             select new RentalDetailDto
                //             {
                //                 Id = r.Id,
                //                 CompanyName = cu.CompanyName,
                //                 CustomerName= u.FirstName+" "+u.LastName,
                //                 DailyPrice = c.DailyPrice,
                //                 Description = c.Description,
                //                 RentDate = r.RentDate,
                //                 ReturnDate = r.ReturnDate,
                //             };

                return result.ToList();
            }


        }
    }
}
