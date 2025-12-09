using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Content.Client.Markers;
using Content.IntegrationTests;
using Content.IntegrationTests.Pair;
using Content.Server.GameTicking;
using Content.Server.Maps;
using Robust.Client.GameObjects;
using Robust.Server.GameObjects;
using Robust.Server.Player;
<<<<<<< HEAD
=======
using Robust.Shared.ContentPack;
using Robust.Shared.EntitySerialization;
using Robust.Shared.EntitySerialization.Systems;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;
using Robust.Shared.Maths;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Content.MapRenderer.Painters
{
    public sealed class MapPainter : IAsyncDisposable
    {
<<<<<<< HEAD
        public static async IAsyncEnumerable<RenderedGridImage<Rgba32>> Paint(string map)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
=======
        private readonly RenderMap _map;
        private readonly ITestContextLike _testContextLike;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

        private TestPair? _pair;
        private Entity<MapGridComponent>[] _grids = [];

        public MapPainter(RenderMap map, ITestContextLike testContextLike)
        {
            _map = map;
            _testContextLike = testContextLike;
        }

        public async Task Initialize()
        {
            var stopwatch = RStopwatch.StartNew();

            var poolSettings = new PoolSettings
            {
                DummyTicker = false,
                Connected = true,
                Destructive = true,
                Fresh = true,
                // Seriously whoever made MapPainter use GameMapPrototype I wish you step on a lego one time.
<<<<<<< HEAD
                Map = map,
            });

            var server = pair.Server;
            var client = pair.Client;
=======
                Map = _map is RenderMapPrototype prototype ? prototype.Prototype : PoolManager.TestMap,
            };
            _pair = await PoolManager.GetServerClient(poolSettings, _testContextLike);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

            Console.WriteLine($"Loaded client and server in {(int)stopwatch.Elapsed.TotalMilliseconds} ms");

            if (_map is RenderMapFile mapFile)
            {
                using var stream = File.OpenRead(mapFile.FileName);

                await _pair.Server.WaitPost(() =>
                {
                    var loadOptions = new MapLoadOptions
                    {
                        // Accept loading both maps and grids without caring about what the input file truly is.
                        DeserializationOptions =
                        {
                            LogOrphanedGrids = false,
                        },
                    };

                    if (!_pair.Server.System<MapLoaderSystem>().TryLoadGeneric(stream, mapFile.FileName, out var loadResult, loadOptions))
                        throw new IOException($"File {mapFile.FileName} could not be read");

                    _grids = loadResult.Grids.ToArray();
                });
            }
        }

        public async Task SetupView(bool showMarkers)
        {
            if (_pair == null)
                throw new InvalidOperationException("Instance not initialized!");

            await _pair.Client.WaitPost(() =>
            {
                if (_pair.Client.EntMan.TryGetComponent(_pair.Client.PlayerMan.LocalEntity, out SpriteComponent? sprite))
                {
                    _pair.Client.System<SpriteSystem>()
                        .SetVisible((_pair.Client.PlayerMan.LocalEntity.Value, sprite), false);
                }
            });

<<<<<<< HEAD
            var sEntityManager = server.ResolveDependency<IServerEntityManager>();
            var sPlayerManager = server.ResolveDependency<IPlayerManager>();

            await pair.RunTicksSync(10);
=======
            if (showMarkers)
            {
                await _pair.Client.WaitPost(() =>
                {
                    _pair.Client.System<MarkerSystem>().MarkersVisible = true;
                });
            }
        }

        public async Task<MapViewerData> GenerateMapViewerData(ParallaxOutput? parallaxOutput)
        {
            if (_pair == null)
                throw new InvalidOperationException("Instance not initialized!");

            var mapShort = _map.ShortName;

            string fullName;
            if (_map is RenderMapPrototype prototype)
            {
                fullName = _pair.Server.ProtoMan.Index(prototype.Prototype).MapName;
            }
            else
            {
                fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mapShort);
            }

            var mapViewerData = new MapViewerData
            {
                Id = mapShort,
                Name = fullName,
            };

            if (parallaxOutput != null)
            {
                await _pair.Client.WaitPost(() =>
                {
                    var res = _pair.Client.InstanceDependencyCollection.Resolve<IResourceManager>();
                    mapViewerData.ParallaxLayers.Add(LayerGroup.DefaultParallax(res, parallaxOutput));
                });
            }

            return mapViewerData;
        }

        public async IAsyncEnumerable<RenderedGridImage<Rgba32>> Paint()
        {
            if (_pair == null)
                throw new InvalidOperationException("Instance not initialized!");

            var client = _pair.Client;
            var server = _pair.Server;

            var sEntityManager = server.ResolveDependency<IServerEntityManager>();
            var sPlayerManager = server.ResolveDependency<IPlayerManager>();

            var entityManager = server.ResolveDependency<IEntityManager>();
            var mapSys = entityManager.System<SharedMapSystem>();

            await _pair.RunTicksSync(10);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
            await Task.WhenAll(client.WaitIdleAsync(), server.WaitIdleAsync());

            var sMapManager = server.ResolveDependency<IMapManager>();

            var tilePainter = new TilePainter(client, server);
            var entityPainter = new GridPainter(client, server);
            Entity<MapGridComponent>[] grids = null!;
            var xformQuery = sEntityManager.GetEntityQuery<TransformComponent>();
            var xformSystem = sEntityManager.System<SharedTransformSystem>();

            await server.WaitPost(() =>
            {
                var playerEntity = sPlayerManager.Sessions.Single().AttachedEntity;

                if (playerEntity.HasValue)
                {
                    sEntityManager.DeleteEntity(playerEntity.Value);
                }

<<<<<<< HEAD
                var mapId = sEntityManager.System<GameTicker>().DefaultMap;
                grids = sMapManager.GetAllGrids(mapId).ToArray();
=======
                if (_map is RenderMapPrototype)
                {
                    var mapId = sEntityManager.System<GameTicker>().DefaultMap;
                    _grids = sMapManager.GetAllGrids(mapId).ToArray();
                }
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

                foreach (var (uid, _) in _grids)
                {
                    var gridXform = xformQuery.GetComponent(uid);
                    xformSystem.SetWorldRotation(gridXform, Angle.Zero);
                }
            });

            await _pair.RunTicksSync(10);
            await Task.WhenAll(client.WaitIdleAsync(), server.WaitIdleAsync());

            foreach (var (uid, grid) in _grids)
            {
                // Skip empty grids
                if (grid.LocalAABB.IsEmpty())
                {
                    Console.WriteLine($"Warning: Grid {uid} was empty. Skipping image rendering.");
                    continue;
                }

                var tileXSize = grid.TileSize * TilePainter.TileImageSize;
                var tileYSize = grid.TileSize * TilePainter.TileImageSize;

                var bounds = grid.LocalAABB;

                var left = bounds.Left;
                var right = bounds.Right;
                var top = bounds.Top;
                var bottom = bounds.Bottom;

                var w = (int) Math.Ceiling(right - left) * tileXSize;
                var h = (int) Math.Ceiling(top - bottom) * tileYSize;

                var gridCanvas = new Image<Rgba32>(w, h);

                await server.WaitPost(() =>
                {
                    tilePainter.Run(gridCanvas, uid, grid);
                    entityPainter.Run(gridCanvas, uid, grid);

                    gridCanvas.Mutate(e => e.Flip(FlipMode.Vertical));
                });

                var renderedImage = new RenderedGridImage<Rgba32>(gridCanvas)
                {
                    GridUid = uid,
                    Offset = xformSystem.GetWorldPosition(uid),
                };

                yield return renderedImage;
            }
        }

        public async Task CleanReturnAsync()
        {
            if (_pair == null)
                throw new InvalidOperationException("Instance not initialized!");

            await _pair.CleanReturnAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_pair != null)
                await _pair.DisposeAsync();
        }
    }
}
