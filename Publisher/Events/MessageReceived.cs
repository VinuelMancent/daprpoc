﻿namespace Publisher.Events;

public record struct MessageReceived(string sender, string message);