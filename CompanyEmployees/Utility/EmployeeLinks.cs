using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Dynamic;


namespace CompanyEmployees.Utility
{
    public class EmployeeLinks : IEmployeeLinks 
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<EmployeeDto> _dataShaper;

        public EmployeeLinks(LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto,
            string fields, Guid companyId, HttpContext httpContext)
        {
            var shapedEmployees = ShapeData(employeesDto, fields);

            if (ShouldGenerateLink(httpContext))
            {
                return ReturnLinkdedEmployees(employeesDto, fields, companyId, httpContext, shapedEmployees);
            }

            return ReturnShapedEmployees(shapedEmployees);
        }

        private List<ExpandoObject> ShapeData(IEnumerable<EmployeeDto> employeesDto, string fields)
            => _dataShaper.ShapeData(employeesDto, fields)
            .Select(e => e.Entity)
            .ToList();

        private bool ShouldGenerateLink(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedEmployees(List<ExpandoObject> shapedEmployees) =>
           new LinkResponse { ShapedEntities = shapedEmployees };

        private LinkResponse ReturnLinkdedEmployees(IEnumerable<EmployeeDto> employeesDto,
            string fields, Guid companyId, HttpContext httpContext, List<ExpandoObject> shapedEmployees)
        {
            var employeesDtoList = employeesDto.ToList();

            for (var index = 0; index < employeesDtoList.Count(); index++)
            {
                var employeeLinks = CreateLinksForEmployee(httpContext, companyId,
                    employeesDtoList[index].Id, fields);
                shapedEmployees[index].TryAdd("Link", employeeLinks);
            }

            var employeeCollection = new LinkCollectionWrapper<ExpandoObject>(shapedEmployees);
            var linkedEmployees = CreateLinksForEmployees(httpContext, employeeCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
        }

        private List<Link> CreateLinksForEmployee(HttpContext httpContext, Guid companyId, Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext,"GetEmployeeForCompany",
                values: new { companyId ,id, fields}),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(httpContext,"DeleteEmployeeForCompany",
                values: new { companyId, id}),
                "delete_employee",
                "DELETE"),

                new Link(_linkGenerator.GetUriByAction(httpContext,"UpdateEmployeeForCompany",
                values: new{ companyId,id}),
                "update_employee",
                "PUT"),

                new Link(_linkGenerator.GetPathByAction(httpContext,"PartiallyUpdateEmployeeForCompany",
                values: new{ companyId, id}),
                "partially_update_employee",
                "PATCH")
            };

            return links;
        }

        private LinkCollectionWrapper<ExpandoObject> CreateLinksForEmployees(HttpContext httpContext, 
            LinkCollectionWrapper<ExpandoObject> employeesWrapper)
        {
            employeesWrapper.Links.Add(new Link(_linkGenerator.GetPathByAction(httpContext,
                "GetEmployeeForCompany",
                values: new { }),
                "self",
                "GET"));

            return employeesWrapper;
        }

    } 
}
