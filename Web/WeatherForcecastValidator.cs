using FluentValidation;

namespace Web {
    public class WeatherForcecastValidator : AbstractValidator<WeatherForecast> {

        public WeatherForcecastValidator() {
            RuleFor(p => p.Date).Must(p=> p != default);
            RuleFor(p => p.TemperatureC).GreaterThan(0);
        }
    }
}
