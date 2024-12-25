using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Converters
{
    internal class DateTimeToDateTimeOffsetConverter : IValueConverter<DateTime, DateTimeOffset>
    {
        public DateTimeOffset Convert(DateTime sourceMember, ResolutionContext context)
        {
            var localTime1 = DateTime.SpecifyKind(sourceMember, DateTimeKind.Local);
            DateTimeOffset localTime2 = localTime1;
            return localTime2;
        }
    }
}
