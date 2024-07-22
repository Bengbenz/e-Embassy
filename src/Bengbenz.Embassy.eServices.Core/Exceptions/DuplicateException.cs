// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.Core.Exceptions;

/// <summary>
/// Represents a custom exception for handling duplicate entity scenarios.
/// </summary>
/// <remarks>
/// This exception is thrown when an attempt is made to create or update an entity
/// in a way that would violate a uniqueness constraint, such as trying to add a new category
/// with a name that already exists.
/// </remarks>
public sealed class DuplicateException(string message) : Exception(message);
