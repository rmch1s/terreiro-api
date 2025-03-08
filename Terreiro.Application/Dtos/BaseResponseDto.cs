namespace Terreiro.Application.Dtos;

public record BaseResponseDto<T>(
    T? Data,
    bool Error,
    string[] ErrorMessages
);