using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Log
{
    public int Id { get; set; }

    public string Message { get; set; }

    public string Typelog { get; set; }

    public string Login { get; set; }

    public DateTime Datelog { get; set; }
}
