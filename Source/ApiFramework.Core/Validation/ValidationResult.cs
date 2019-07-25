// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiFramework.Validation
{
    /// <summary>
    /// Represents the result of a validation operation which is the aggregation of all <see cref="ValidationError"/> objects if any.
    /// Validation does not stop on the first error, instead accumulates all the validation errors.
    /// No validation errors represents success, conversely any validation errors represents failed validation.
    /// </summary>
    public class ValidationResult
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a validation result that represents success.</summary>
        public ValidationResult()
        { }

        /// <summary>Creates a validation result that represents failure with the validation errors providing context for the failure.</summary>
        /// <param name="errors"></param>
        public ValidationResult(IEnumerable<ValidationError> errors)
        { this.Errors = errors ?? Enumerable.Empty<ValidationError>(); }

        /// <summary>Creates a validation result that represents failure with the validation errors providing context for the failure.</summary>
        /// <param name="errors"></param>
        public ValidationResult(params ValidationError[] errors)
        { this.Errors = errors ?? Enumerable.Empty<ValidationError>(); }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets predicate if the validation result represents success or not.</summary>
        public bool IsValid => this.Errors == null || this.Errors.Any() == false;

        /// <summary>Gets the validation errors count.</summary>
        public int ErrorsCount => this.Errors?.Count() ?? 0;

        /// <summary>Gets the validation errors.</summary>
        /// <remarks>Will be null or empty for a successful validation result, otherwise non-null and contains validation errors for a failed validation result.</remarks>
        public IEnumerable<ValidationError> Errors { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{typeof(ValidationResult).Name} [valid={this.IsValid} count={this.ErrorsCount}]");
            foreach (var error in this.Errors ?? Enumerable.Empty<ValidationError>())
            {
                stringBuilder.AppendLine($"  {error}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
        #endregion

        #region Factory Methods
        /// <summary>Aggregates a collection of validation results into a single validation result by collecting all the validation errors into a single collection.</summary>
        /// <param name="results">Collection of validation results to aggregate into a single validation result.</param>
        /// <returns>Aggregation of all the validation results into a single validation result.</returns>
        public static ValidationResult Aggregate(IEnumerable<ValidationResult> results)
        {
            var resultsCollection = results.SafeToReadOnlyCollection();

            if (resultsCollection.All(x => x.IsValid))
                return Success;

            var errors = resultsCollection.Where(x => x.Errors != null)
                                          .SelectMany(x => x.Errors)
                                          .ToList();
            return new ValidationResult(errors);
        }

        /// <summary>Aggregates a collection of validation results into a single validation result by collecting all the validation errors into a single collection.</summary>
        /// <param name="results">Collection of validation results to aggregate into a single validation result.</param>
        /// <returns>Aggregation of all the validation results into a single validation result.</returns>
        public static ValidationResult Aggregate(params ValidationResult[] results)
        { return Aggregate(results.AsEnumerable()); }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        /// <summary>Gets a successful validation result.</summary>
        public static readonly ValidationResult Success = new ValidationResult();
        #endregion
    }
}
