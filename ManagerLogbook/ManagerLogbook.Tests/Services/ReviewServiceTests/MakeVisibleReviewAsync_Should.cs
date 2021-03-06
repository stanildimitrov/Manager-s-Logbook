﻿using ManagerLogbook.Data;
using ManagerLogbook.Services;
using ManagerLogbook.Services.Contracts.Providers;
using ManagerLogbook.Services.CustomExeptions;
using ManagerLogbook.Services.Utils;
using ManagerLogbook.Tests.HelpersMethods;
using ManagerLogbook.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ManagerLogbook.Tests.Services.ReviewServiceTests
{
    [TestClass]
    public class MakeVisibleReviewAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnMakeVisibleReview()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnMakeVisibleReview));

            using (var arrangeContext = new ManagerLogbookContext(options))
            {
                await arrangeContext.BusinessUnits.AddAsync(TestHelperReview.TestBusinessUnit01());
                await arrangeContext.Reviews.AddAsync(TestHelperReview.Review01());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new ManagerLogbookContext(options))
            {
                var mockBusinessValidator = new Mock<IBusinessValidator>();
                var mockReviewEditor = new Mock<IReviewEditor>();

                var sut = new ReviewService(assertContext, mockBusinessValidator.Object, mockReviewEditor.Object);

                var review = await sut.MakeInVisibleReviewAsync(1);
                                
                Assert.AreEqual(review.isVisible, false);
            }
        }

        [TestMethod]
        public async Task ThrowsExeptionByMakeVisibleWhenReviewWasNotFound()
        {
            var options = TestUtils.GetOptions(nameof(ThrowsExeptionByMakeVisibleWhenReviewWasNotFound));

            using (var arrangeContext = new ManagerLogbookContext(options))
            {
                await arrangeContext.BusinessUnits.AddAsync(TestHelperReview.TestBusinessUnit01());
                await arrangeContext.Reviews.AddAsync(TestHelperReview.Review01());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new ManagerLogbookContext(options))
            {
                var mockBusinessValidator = new Mock<IBusinessValidator>();
                var mockReviewEditor = new Mock<IReviewEditor>();

                var sut = new ReviewService(assertContext, mockBusinessValidator.Object, mockReviewEditor.Object);

                var ex = await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.MakeInVisibleReviewAsync(2));

                Assert.AreEqual(ex.Message, string.Format(ServicesConstants.ReviewNotFound));
            }
        }
    }
}
