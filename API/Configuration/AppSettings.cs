﻿using Api.Configuration;

namespace Api;

public class AppSettings
{
    public DbConfig ConnectionStrings { get; set; }
    public JWTOptions JWTOptions { get; set; }
}
