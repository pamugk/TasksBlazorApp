﻿using Microsoft.AspNetCore.Components;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TasksSpa.Model;
using TasksSpa.Services.Contracts;

namespace TasksSpa.Services.Implementation
{
    public class TaskManagerApi : ITaskManagerApi
    {
        private readonly HttpClient httpClient;
        private const string server = "https://todos-rest-api-server.herokuapp.com";

        public TaskManagerApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task ChangeStatus(TaskDto changedTask, string token)
        {
            TaskDto clone = new TaskDto()
            {
                Id = changedTask.Id,
                UserId = changedTask.UserId,
                Title = changedTask.Title,
                Description = changedTask.Description,
                IsFinished = !changedTask.IsFinished
            };
            await UpdateTask(clone, token);
        }

        public async Task<int> CreateTask(TaskDto newTask, string token)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(newTask), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{server}/api/tasks?token={WebUtility.UrlEncode(token)}", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(body);
            return JsonSerializer.Deserialize<int>(body);
        }

        public async Task<CommentDto[]> GetComments(int taskId)
        {
            return await httpClient.GetJsonAsync<CommentDto[]>(
                $"https://jsonplaceholder.typicode.com/comments?postId={taskId}");
        }

        public async Task<TaskDto> GetTask(int taskId, string token)
        {
            var response = await httpClient.GetAsync($"{server}/api/tasks/{taskId}?token={WebUtility.UrlEncode(token)}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<TaskDto>(body);
            else throw new Exception(body);
        }

        public async Task<TaskDto[]> GetTasks(string token)
        {
            var response = await httpClient.GetAsync($"{server}/api/tasks?token={WebUtility.UrlEncode(token)}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<TaskDto[]>(body);
            else throw new Exception(body);
        }

        public async Task RemoveTask(int taskId, string token)
        {
            var response = await httpClient.DeleteAsync($"{server}/api/tasks/{taskId}?token={WebUtility.UrlEncode(token)}");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(body);
        }

        public async Task UpdateTask(TaskDto updatedTask, string token)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(updatedTask), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{server}/api/tasks/add?token={WebUtility.UrlEncode(token)}", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(body);
        }
    }
}