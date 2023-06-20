﻿using System;
using System.Collections.Generic;

namespace CI_API.Core.Models;

public partial class Story
{
    public long StoryId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? Theme { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? Views { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual ICollection<StoryInvite> StoryInvites { get; set; } = new List<StoryInvite>();

    public virtual ICollection<StoryMedium> StoryMedia { get; set; } = new List<StoryMedium>();

    public virtual User User { get; set; } = null!;
}
