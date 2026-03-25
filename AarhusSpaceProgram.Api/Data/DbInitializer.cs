using AarhusSpaceProgram.Api.Models;

namespace AarhusSpaceProgram.Api.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Missions.Any())
        {
            return;
        }

        var astronauts = new List<Astronaut>
        {
            new() { FullName = "Emma Carter", HoursInSpace = 1200 },
            new() { FullName = "Noah Rivera", HoursInSpace = 850 },
            new() { FullName = "Liam Andersen", HoursInSpace = 430 }
        };

        var scientists = new List<Scientist>
        {
            new() { FullName = "Dr. Sofia Nguyen", FieldOfExpertise = "Astrobiology" },
            new() { FullName = "Dr. Mikkel Jensen", FieldOfExpertise = "Planetary Geology" }
        };

        var managers = new List<Manager>
        {
            new() { FullName = "Sarah Mitchell", Department = "Mission Operations" },
            new() { FullName = "Jonas Petersen", Department = "Flight Planning" }
        };

        var rockets = new List<Rocket>
        {
            new() { Model = "Falcon A1", Weight = 540000, Manufacturer = "Aarhus Aerospace" },
            new() { Model = "Orion Heavy", Weight = 720000, Manufacturer = "Nordic Launch Systems" }
        };

        var launchpads = new List<Launchpad>
        {
            new() { Location = "Aarhus Launch Complex A", Description = "Primary coastal launchpad" },
            new() { Location = "Aarhus Launch Complex B", Description = "Secondary inland launchpad" }
        };

        var celestialBodies = new List<CelestialBody>
        {
            new() { Name = "Mars", Type = "Planet" },
            new() { Name = "Moon", Type = "Moon" },
            new() { Name = "Europa", Type = "Moon" }
        };

        context.Astronauts.AddRange(astronauts);
        context.Scientists.AddRange(scientists);
        context.Managers.AddRange(managers);
        context.Rockets.AddRange(rockets);
        context.Launchpads.AddRange(launchpads);
        context.CelestialBodies.AddRange(celestialBodies);

        context.SaveChanges();

        var missions = new List<Mission>
        {
            new()
            {
                Name = "Mars Recon I",
                LaunchDate = new DateOnly(2027, 3, 15),
                Status = MissionStatus.Planned,
                ManagerId = managers[0].Id,
                RocketId = rockets[0].Id,
                LaunchpadId = launchpads[0].Id,
                TargetCelestialBodyId = celestialBodies[0].Id,
                Astronauts = new List<Astronaut> { astronauts[0], astronauts[1] },
                Scientists = new List<Scientist> { scientists[0] }
            },
            new()
            {
                Name = "Europa Survey",
                LaunchDate = new DateOnly(2027, 6, 10),
                Status = MissionStatus.Created,
                ManagerId = managers[1].Id,
                RocketId = rockets[1].Id,
                LaunchpadId = launchpads[1].Id,
                TargetCelestialBodyId = celestialBodies[2].Id,
                Astronauts = new List<Astronaut> { astronauts[2] },
                Scientists = new List<Scientist> { scientists[1] }
            }
        };

        context.Missions.AddRange(missions);
        context.SaveChanges();
    }
}
