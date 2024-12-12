namespace Framework.DomainRules.Policies;

public interface IDomainPolicyBuilder<in T>
{
    DomainPolicy Build(T value);
}