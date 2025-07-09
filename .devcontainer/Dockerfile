# Base image with .NET SDK 8.0 (and runtime)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Set the working directory inside the container
WORKDIR /workspace

# Install any additional tools if needed (optional)
RUN apt-get update && apt-get install -y \
    curl python3\
    git \
    && rm -rf /var/lib/apt/lists/*

ENV PYTHONDONTWRITEBYTECODE=1 \
    PYTHONUNBUFFERED=1

RUN curl -LsSf https://astral.sh/uv/install.sh | sh \
    && mv /root/.cargo/bin/uv /usr/local/bin/uv

COPY pyproject.toml uv.lock ./

RUN uv sync --extra docs --frozen

# Default command
CMD [ "bash" ]
