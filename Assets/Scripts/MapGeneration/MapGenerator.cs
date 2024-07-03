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

        [Header("Ground Plates")]
        [SerializeField] private Transform _platePrefab;
        [SerializeField] private int _spacing;
        [SerializeField] private List<Transform> _plates;

        #endregion

        #region Public

        public void UpdatePlates()
        {
            ClearPlates();

            for (int x = (int)-_size.x / 2; x < _size.x / 2; x++)
            {
                for (int y = (int)-_size.y / 2; y < _size.y / 2; y++)
                {
                    var plate = Instantiate(_platePrefab,
                                            new Vector3(x * _spacing, 0, y * _spacing),
                                            Quaternion.identity,
                                            this.transform);
                    _plates.Add(plate);
                }
            }
        }

        public void ClearPlates()
        {
            foreach (var item in _plates)
            {
                DestroyImmediate(item.gameObject);
            }
            _plates.Clear();
        }

        #endregion
    }
}
