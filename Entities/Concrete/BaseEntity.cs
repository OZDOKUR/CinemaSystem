using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
    public int Status { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public DateTime DeletedTime { get; set; }
}
