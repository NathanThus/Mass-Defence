using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NathanThus.MassDefence.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        #region Serialized Fields
        [Header("Settings")]
        [SerializeField] private Vector2 _size;
        [SerializeField] private int _spacing;
        [SerializeField] private List<Transform> _tiles;


        [Header("Ground Plates")]
        [SerializeField] private Transform _basicTile;
        [SerializeField] private Transform _startTile;
        [SerializeField] private Transform _goalTile;
        [SerializeField] private Transform _plateHolder;

        #endregion

        #region Properties

        public static bool IsInitialized = false;
        public static Vector3 StartPosition = Vector3.zero;
        public static Vector3 GoalPosition = Vector3.zero;

        #endregion

        #region Public

        public void UpdateTiles()
        {
            bool isValidLocationPair = false;
            Vector2 startPos = Vector2.zero, goalPos = Vector2.zero;
            while (!isValidLocationPair)
            {
                startPos = new((int)Random.Range(-_size.x / 2, _size.x / 2), (int)Random.Range(-_size.y / 2, _size.y / 2));
                goalPos = new((int)Random.Range(-_size.x / 2, _size.x / 2), (int)Random.Range(-_size.y / 2, _size.y / 2));

                if (Vector2.Distance(startPos, goalPos) > _spacing) isValidLocationPair = true;
            }

            GoalPosition = new Vector3(goalPos.x * _spacing, 0, goalPos.y * _spacing);
            StartPosition = new Vector3(startPos.x * _spacing, 0, startPos.y * _spacing);

            ClearTiles();

            for (int x = (int)-_size.x / 2; x < _size.x / 2; x++)
            {
                for (int y = (int)-_size.y / 2; y < _size.y / 2; y++)
                {
                    if (x == startPos.x && y == startPos.y)
                    {
                        MoveTile(_startTile, x, y);
                    }
                    else if (x == goalPos.x && y == goalPos.y)
                    {
                        MoveTile(_goalTile, x, y);
                    }
                    else
                    {
                        CreateTile(_basicTile, x, y);
                    }
                }
            }
            IsInitialized = true;
        }

        private void CreateTile(Transform tile, int x, int y)
        {
            var newTile = Instantiate(tile,
                                    new Vector3(x * _spacing, 0, y * _spacing),
                                    Quaternion.identity,
                                    _plateHolder);
            _tiles.Add(newTile);
        }

        private void MoveTile(Transform tile, int x, int y)
        {
            tile.position = new Vector3(x * _spacing, 0, y * _spacing);
        }

        public void ClearTiles()
        {
            foreach (var item in _tiles)
            {
                DestroyImmediate(item.gameObject);
            }
            _tiles.Clear();
        }

        #endregion
    }
}
