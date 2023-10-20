using AutoMapper;

namespace PreubaLogics.Extensions.Operations
{
    public static class MapperExtensions
    {
        public static TDestination Map<TSource1, TSource2, TDestination>(this IMapper mapper, TSource1 source1, TSource2 source2)
        {
            TDestination? destination = mapper.Map<TSource1, TDestination>(source1);
            return mapper.Map(source2, destination);
        }

        public static TDestination Map<TDestination>(this IMapper mapper, params object[] sources) where TDestination : new()
        {

            return Map(mapper, new TDestination(), sources);
        }

        public static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, params object[] sources) where TDestination : new()
        {


            if (!sources.Any())
            {
                return destination;
            }

            foreach (object src in sources)
            {


                destination = mapper.Map(src, destination);


            }

            return destination;
        }
        public static IMappingExpression<TSource, TDestination> ForAllMembersIfNotEmpty<TSource, TDestination>(
         this IMappingExpression<TSource, TDestination> expression)
        {
            ForAllMembers(expression);
            var newExpresion = expression.ReverseMap();
            ForAllMembers(newExpresion);
            return expression;
        }
        public static IMappingExpression ForAllMembersIfNotEmpty(
    this IMappingExpression expression)
        {
            ForAllMembers(expression);
            IMappingExpression newExpression = expression.ReverseMap();
            ForAllMembers(newExpression);
            return expression;
        }
        public static IMappingExpression Includes(this IMappingExpression typeMapExpression, TypeMap typeMapConfiguration)
        {
            foreach (PropertyMap? propertyMap in typeMapConfiguration.PropertyMaps)
            {
                if (propertyMap.SourceMember != null)
                {
                    _ = typeMapExpression.ForMember(propertyMap.DestinationMember.Name, opt =>
                    {
                        opt.MapFrom(propertyMap.SourceMember.Name);


                    });
                }
            }

            return typeMapExpression;
        }
        private static void ForAllMembers(IMappingExpression expression)
        {
            expression.ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
            {
                return srcMember != null && ((srcMember is not int && srcMember is not decimal) || Convert.ToDecimal(srcMember) != 0);
            }));
        }
        private static void ForAllMembers<TSource, TDestination>(IMappingExpression<TSource, TDestination> expression)
        {
            expression.ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
            {
                return srcMember != null && ((srcMember is not int && srcMember is not decimal) || Convert.ToDecimal(srcMember) != 0);
            }));
        }
    }

}
