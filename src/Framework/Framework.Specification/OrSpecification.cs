﻿namespace Framework.Specification;

public class OrSpecification<T>(ISpecification<T> left, ISpecification<T> right) : Specification<T>
{
    private readonly ISpecification<T> left = left;
    private readonly ISpecification<T> right = right;

    public override bool IsSatisfiedBy(T value)
    {
        return left.IsSatisfiedBy(value) || right.IsSatisfiedBy(value);
    }
}