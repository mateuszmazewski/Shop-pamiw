using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Tests
{
    [TestClass]
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly CustomerService _customerService;
        private List<Customer> exampleCustomers;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);

            exampleCustomers = new List<Customer>();
            exampleCustomers.Add(new Customer()
            {
                Id = 1,
                Name = "Jan",
                Surname = "Kowalski",
                Address = "ul. Cicha 132/16, 62 - 200 Gniezno",
                DateOfBirth = new DateTime(2000, 5, 1)
            });
            exampleCustomers.Add(new Customer()
            {
                Id = 2,
                Name = "Pawe³",
                Surname = "Nowak",
                Address = "ul. Miodowa 3/6, 00-246 Warszawa",
                DateOfBirth = new DateTime(1989, 2, 16)
            });
            exampleCustomers.Add(new Customer()
            {
                Id = 3,
                Name = "Alicja",
                Surname = "Nowicka",
                Address = "ul. Weso³a 8/18, 86-300 Grudzi¹dz",
                DateOfBirth = new DateTime(1995, 8, 19)
            });
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task Get_ShouldReturnCustomer_WhenCustomerWithGivenIdExists()
        {
            int id = 3;
            string name = "Jan";
            string surname = "Kowalski";
            string address = "ul. Cicha 132/16, 62 - 200 Gniezno";
            DateTime dateOfBirth = new DateTime(2000, 5, 1);
            var customer = new Customer()
            {
                Id = id,
                Name = name,
                Surname = surname,
                Address = address,
                DateOfBirth = dateOfBirth
            };
            _customerRepositoryMock.Setup(x => x.GetAsync(id)).ReturnsAsync(customer);

            var customerDTO = await _customerService.Get(id);

            Assert.AreEqual(id, customerDTO.Id);
            Assert.AreEqual(name, customerDTO.Name);
            Assert.AreEqual(surname, customerDTO.Surname);
            Assert.AreEqual(address, customerDTO.Address);
            Assert.AreEqual(dateOfBirth, customerDTO.DateOfBirth);
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task Get_ShouldReturnNull_WhenCustomerWithGivenIdDoesNotExist()
        {
            int id = 33;
            _customerRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var customerDTO = await _customerService.Get(id);

            Assert.IsNull(customerDTO);
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAll_ShouldReturnListOfAllCustomers_WhenCustomersExist()
        {
            List<Customer> customers = exampleCustomers;
            int expectedCount = customers.Count;
            _customerRepositoryMock.Setup(x => x.BrowseAllAsync()).ReturnsAsync(customers);

            var customerDTOs = (await _customerService.BrowseAll()).ToList();

            Assert.AreEqual(expectedCount, customerDTOs.Count);
            for (int i = 0; i < expectedCount; i++)
            {
                Assert.AreEqual(customers[i].Id, customerDTOs[i].Id);
                Assert.AreEqual(customers[i].Name, customerDTOs[i].Name);
                Assert.AreEqual(customers[i].Surname, customerDTOs[i].Surname);
                Assert.AreEqual(customers[i].Address, customerDTOs[i].Address);
                Assert.AreEqual(customers[i].DateOfBirth, customerDTOs[i].DateOfBirth);
            }
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAll_ShouldReturnNull_WhenCustomersDoNotExist()
        {
            _customerRepositoryMock.Setup(x => x.BrowseAllAsync()).ReturnsAsync(() => null);

            var customerDTOs = await _customerService.BrowseAll();

            Assert.IsNull(customerDTOs);
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAllByFilter_ShouldReturnListOfFilteredCustomers_WhenBothFiltersAreNotEmpty()
        {
            List<Customer> customers = exampleCustomers;
            string name = customers[0].Name;
            string surname = customers[0].Surname;
            _customerRepositoryMock.Setup(x => x.BrowseAllByFilterAsync(name, surname)).ReturnsAsync(customers.GetRange(0, 1));

            var customerDTOs = (await _customerService.BrowseAllByFilter(name, surname)).ToList();

            foreach (var customerDTO in customerDTOs)
            {
                Assert.IsTrue(customerDTO.Name.Contains(name));
                Assert.IsTrue(customerDTO.Surname.Contains(surname));
            }
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAllByFilter_ShouldReturnListOfFilteredCustomers_WhenOnlyNameFilterIsNotEmpty()
        {
            List<Customer> customers = exampleCustomers;
            string name = customers[0].Name;
            string surname = null;
            _customerRepositoryMock.Setup(x => x.BrowseAllByFilterAsync(name, surname)).ReturnsAsync(customers.GetRange(0, 1));

            var customerDTOs = (await _customerService.BrowseAllByFilter(name, surname)).ToList();

            foreach (var customerDTO in customerDTOs)
            {
                Assert.IsTrue(customerDTO.Name.Contains(name));
            }
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAllByFilter_ShouldReturnListOfFilteredCustomers_WhenOnlySurnameFilterIsNotEmpty()
        {
            List<Customer> customers = exampleCustomers;
            string name = null;
            string surname = customers[0].Surname;
            _customerRepositoryMock.Setup(x => x.BrowseAllByFilterAsync(name, surname)).ReturnsAsync(customers.GetRange(0, 1));

            var customerDTOs = (await _customerService.BrowseAllByFilter(name, surname)).ToList();

            foreach (var customerDTO in customerDTOs)
            {
                Assert.IsTrue(customerDTO.Surname.Contains(surname));
            }
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAllByFilter_ShouldReturnListOfAllCustomers_WhenBothFiltersAreEmpty()
        {
            List<Customer> customers = exampleCustomers;
            int expectedCount = customers.Count;
            string name = null;
            string surname = null;
            _customerRepositoryMock.Setup(x => x.BrowseAllByFilterAsync(name, surname)).ReturnsAsync(customers);

            var customerDTOs = (await _customerService.BrowseAllByFilter(name, surname)).ToList();

            Assert.AreEqual(expectedCount, customerDTOs.Count);
            for (int i = 0; i < expectedCount; i++)
            {
                Assert.AreEqual(customers[i].Id, customerDTOs[i].Id);
                Assert.AreEqual(customers[i].Name, customerDTOs[i].Name);
                Assert.AreEqual(customers[i].Surname, customerDTOs[i].Surname);
                Assert.AreEqual(customers[i].Address, customerDTOs[i].Address);
                Assert.AreEqual(customers[i].DateOfBirth, customerDTOs[i].DateOfBirth);
            }
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task BrowseAllByFilter_ShouldReturnNull_WhenCustomersDoNotExist()
        {
            string name = "Jacek";
            string surname = "Kowalski";
            _customerRepositoryMock.Setup(x => x.BrowseAllByFilterAsync(name, surname)).ReturnsAsync(() => null);

            var customerDTOs = (await _customerService.BrowseAllByFilter(name, surname));

            Assert.IsNull(customerDTOs);
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task Add_ShouldCallAddMethodFromRepoAndReturnNothing_WhenCustomerIsNotNull()
        {
            var customer = exampleCustomers[0];
            _customerRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<Customer>())).Verifiable();

            await _customerService.Add(customer);

            _customerRepositoryMock.Verify(x => x.AddAsync(It.IsNotNull<Customer>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exception")]
        [Owner("Mateusz")]
        public async Task Add_ShouldThrowArgumentNullException_WhenCustomerIsNull()
        {
            Customer customer = null;
            _customerRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<Customer>())).Verifiable();

            await _customerService.Add(customer);

            Assert.Fail("ArgumentNullException should have been thrown");
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task Delete_ShouldCallDelMethodFromRepoAndReturnNothing_WhenAnyIdGiven()
        {
            int id = 2;
            _customerRepositoryMock.Setup(x => x.DelAsync(It.IsAny<int>())).Verifiable();

            await _customerService.Delete(id);

            _customerRepositoryMock.Verify(x => x.DelAsync(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        [TestCategory("Positive")]
        [Owner("Mateusz")]
        public async Task Update_ShouldCallUpdateMethodFromRepoAndReturnNothing_WhenCustomerIsNotNull()
        {
            var customer = exampleCustomers[0];
            _customerRepositoryMock.Setup(x => x.UpdateAsync(It.IsNotNull<Customer>())).Verifiable();

            await _customerService.Update(customer);

            _customerRepositoryMock.Verify(x => x.UpdateAsync(It.IsNotNull<Customer>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exception")]
        [Owner("Mateusz")]
        public async Task Update_ShouldThrowArgumentNullException_WhenCustomerIsNull()
        {
            Customer customer = null;
            _customerRepositoryMock.Setup(x => x.UpdateAsync(It.IsNotNull<Customer>())).Verifiable();

            await _customerService.Update(customer);

            Assert.Fail("ArgumentNullException should have been thrown");
        }

    }
}
