﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.CommentDTOs;

public record CommentCreateDTO(string Content, int MovieId, string AppUserId);

