﻿namespace api.Application.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}
