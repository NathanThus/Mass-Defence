using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private Transform _startPrefab;
        [SerializeField] private Transform _goalPrefab;
        [SerializeField] private Transform _plateHolder;

        #endregion

        #region Fields

        private Transform _startTile;
        private Transform _goalTile;

        #endregion

        #region Properties
        public Transform StartTile => _startTile;
        public Transform GoalTile => _goalTile;

        #endregion

        #region Public

        public void UpdateTiles()
        {
            bool isValidLocationPair = false;
            Vector2 startPos = Vector2.zero, endPos = Vector2.zero;
            while (!isValidLocationPair)
            {
                startPos = new((int)Random.Range(-_size.x / 2, _size.x / 2), (int)Random.Range(-_size.y / 2, _size.y / 2));
                endPos = new((int)Random.Range(-_size.x / 2, _size.x / 2), (int)Random.Range(-_size.y / 2, _size.y / 2));

                if (Vector2.Distance(startPos, endPos) > _spacing) isValidLocationPair = true;
            }

            ClearTiles();

            for (int x = (int)-_size.x / 2; x < _size.x / 2; x++)
            {
                for (int y = (int)-_size.y / 2; y < _size.y / 2; y++)
                {
                    if (x == startPos.x && y == startPos.y)
                    {
                        CreateStartTile(_startPrefab, x, y);
                    }
                    else if (x == endPos.x && y == endPos.y)
                    {
                        CreateEndTile(_goalPrefab, x, y);
                    }
                    else
                    {
                        _ = CreateTile(_basicTile, x, y);
                    }
                }
            }


        }

        private void CreateEndTile(Transform endTile, int x, int y)
        {
            _goalTile = CreateTile(endTile,x,y);
            _goalTile.parent = transform;
        }

        private void CreateStartTile(Transform startTile, int x, int y)
        {
            _startTile = CreateTile(startTile,x,y);
            _startTile.parent = transform;
        }

        private Transform CreateTile(Transform tile, int x, int y)
        {
            var newTile = Instantiate(tile,
                                    new Vector3(x * _spacing, 0, y * _spacing),
                                    Quaternion.identity,
                                    _plateHolder);
            _tiles.Add(newTile);
            return newTile;
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
