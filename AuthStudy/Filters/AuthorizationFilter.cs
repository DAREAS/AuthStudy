using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Moon.OData;
using Moon.OData.Sql;

namespace AuthStudy.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           // V2.Program.Run();

            string[] primeiroAcesso = {"Logar", "Index"};

            if (!primeiroAcesso.Contains(context.ActionDescriptor.RouteValues["action"]) &&
                context.ActionDescriptor.RouteValues["controller"] != "Login")
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                    new RedirectToPageResult("Home");
            }
        }
    }

    public class V1
    {
        public class MemberRequest
        {
            public string Name { get; set; }
        }

        public class Member
        {
            public string Name { get; set; }
        }
        
        public interface IOrderByField
        {
            List<string> AllowedFields { get; }
        }

        public enum OrderByEnum
        {
            Asc,
            Desc
        }

        public interface IMapperOrderByRequestToModel<TRequest, TModel>
        {
            IDictionary<string, string> Mappers { get; }
        }

        public abstract class MapperOrderByRequestToModel<TRequest, TModel> : IMapperOrderByRequestToModel<TRequest, TModel>
        {
            public IDictionary<string, string> Mappers { get; private set; }

            protected void AddMapper(Expression<Func<TRequest, object>> requestExpression, Expression<Func<TRequest, object>> modelExpression)
            {
                Mappers.Add(((MemberExpression)requestExpression.Body).Member.Name, ((MemberExpression)modelExpression.Body).Member.Name);
            }
        }

        public class MemberMapperOrderByRequestToModel : MapperOrderByRequestToModel<MemberRequest, Member>
        {
            public MemberMapperOrderByRequestToModel()
            {
                AddMapper(request => request.Name, model => model.Name);
            }
        }

        public class OrderByGenerator<TRequest, TModel>
        {
            public IDictionary<string, OrderByEnum> Generate(IMapperOrderByRequestToModel<TRequest, TModel> mapperOrderBy, string orderBy)
            {
                var sentences = new Dictionary<string, OrderByEnum>();

                // Separate fields
                var fieldsOfFilter = orderBy.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string field in fieldsOfFilter)
                {
                    // Separate field property name and value from sort order
                    var fieldSplit = field.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (fieldSplit.Length == 2)
                    {
                        sentences.Add(mapperOrderBy.Mappers[fieldSplit[0]], Enum.Parse<OrderByEnum>(fieldSplit[1]));
                    }
                    else
                    {
                        sentences.Add(mapperOrderBy.Mappers[fieldSplit[0]], OrderByEnum.Asc);
                    }
                }

                return sentences;
            }
        }

        public class Program
        {
            public static void Run()
            {
                var senteces = new OrderByGenerator<MemberRequest, Member>().Generate(new MemberMapperOrderByRequestToModel(), "orderBy");
            }
        }
    }

    public class V2
    {
        public class MemberRequest
        {
            public string Name { get; set; }
        }

        public class Member
        {
            public string Name { get; set; }
        }

        //// Campos 
        public enum MemberOrderByEnum
        {
            Name
        }

        public interface IOrderByField
        {
            List<string> AllowedFields { get; }
        }

        public enum OrderByEnum
        {
            Asc,
            Desc
        }

        public interface IMapperOrderByRequestToModel<TRequest, TOrderByEnum>
        {
            IDictionary<string, TOrderByEnum> Mappers { get; }
        }

        public abstract class MapperOrderByRequestToModel<TRequest, TOrderByEnum> : IMapperOrderByRequestToModel<TRequest, TOrderByEnum>
        {
            public IDictionary<string, TOrderByEnum> Mappers { get; } = new Dictionary<string, TOrderByEnum>(StringComparer.OrdinalIgnoreCase);

            protected void AddMapper(Expression<Func<TRequest, object>> requestExpression, TOrderByEnum orderByEnum)
            {
                Mappers.Add(((MemberExpression)requestExpression.Body).Member.Name, orderByEnum);
            }
        }

        public class MemberMapperOrderByRequestToModel : MapperOrderByRequestToModel<MemberRequest, MemberOrderByEnum>
        {
            public MemberMapperOrderByRequestToModel()
            {
                AddMapper(request => request.Name, MemberOrderByEnum.Name);
            }
        }

        public class OrderByGenerator<TRequest, TOrderByEnum>
        {
            public IDictionary<TOrderByEnum, OrderByEnum> Generate(IMapperOrderByRequestToModel<TRequest, TOrderByEnum> mapperOrderBy, string orderBy)
            {
                var sentences = new Dictionary<TOrderByEnum, OrderByEnum>();

                // Separate fields
                var fieldsOfFilter = orderBy.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string field in fieldsOfFilter)
                {
                    // Separate field property name and value from sort order
                    var fieldSplit = field.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (fieldSplit.Length == 2)
                    {
                        sentences.Add(mapperOrderBy.Mappers[fieldSplit[0]], Enum.Parse<OrderByEnum>(fieldSplit[1], true));
                    }
                    else
                    {
                        sentences.Add(mapperOrderBy.Mappers[fieldSplit[0]], OrderByEnum.Asc);
                    }
                }

                return sentences;
            }
        }

        public static class Program
        {
            public static void Run()
            {
                var senteces = new OrderByGenerator<MemberRequest, MemberOrderByEnum>().Generate(new MemberMapperOrderByRequestToModel(), "Name desc");
                var orderBy = new MemberRepository().List(senteces);
            }
        }

        public class MemberRepository
        {
            public string List(IDictionary<MemberOrderByEnum, OrderByEnum> orderByFields)
            {
                var optionsQuery = new ODataOptions<Member>(new Dictionary<string, string>
                {
                    ["$orderby"] = string.Join(", ", orderByFields.Select(x => $"{x.Key} {x.Value}"))
                })
                {
                    IsCaseSensitive = false
                };

                var orderBy = OrderByClause.Build(optionsQuery, ResolveColumn);

                return orderBy;
            }

            private static string ResolveColumn(PropertyInfo propertyInfo)
            {
                if (propertyInfo.Name.Equals(nameof(Member.Name), StringComparison.OrdinalIgnoreCase))
                {
                    return "m.NameOfMember";
                }

                return propertyInfo.Name;
            }
        }
    }
}
