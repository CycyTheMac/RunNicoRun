using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	// Inspired from https://www.youtube.com/playlist?list=PL67XFC3MYQ6K7rXSnUpWXV844jdd3X7Lq

	#region SubClasses

	[System.Serializable]
	public class Cell {
		public bool Visited;
		public GameObject North;
		public GameObject South;
		public GameObject West;
		public GameObject East;
	}

	#endregion

	#region Variables

	[Header( "Maze:" )]
	[SerializeField] private GameObject WallHolder;
	[SerializeField] [Range( 1 , 50 )] private int MazeX = 10;
	[SerializeField] [Range( 1 , 50 )] private int MazeZ = 10;

	private Cell[] Cells;
	private int CellsCount = 0;

	public int currentCell = 0;

	[Header( "Walls:" )]
	[SerializeField] private GameObject Wall;
	[SerializeField] private float WallWidth = 0.10f;
	[SerializeField] private float WallLength = 1.00f;

	private Vector3 InitialPosition;

	#endregion


	#region Unity Functions

	private void Start() {
		CellsCount = MazeX * MazeZ;
		CreateWalls();
	}

	private void Update() {

	}

	#endregion

	#region Wall Building

	private void CreateWalls() {
		// GameObject temporaryWall;

		// Center on world
		Vector3 myPosition = InitialPosition = new Vector3( ( WallWidth - MazeX ) / 2 , 0 , ( WallLength - MazeZ ) / 2 );


		// X Axis
		for ( var x = 0 ; x < MazeX ; x++ ) {
			for ( var z = 0 ; z <= MazeZ ; z++ ) {
				myPosition = new Vector3( InitialPosition.x + WallLength * ( z - 0.5f ) , 0 , InitialPosition.z + WallLength * ( x - 0.5f ) );
				Instantiate( Wall , myPosition , Quaternion.identity , WallHolder.transform );
			}
		}

		// Z Axis
		for ( var x = 0 ; x <= MazeX ; x++ ) {
			for ( var z = 0 ; z < MazeZ ; z++ ) {
				myPosition = new Vector3( InitialPosition.x + WallLength * z , 0 , InitialPosition.z + WallLength * ( x - 1 ) );
				Instantiate( Wall , myPosition , Quaternion.Euler( 0 , 90 , 0 ) , WallHolder.transform );
			}
		}

		CreateCells();

	}

	#endregion

	#region Cell Assignment

	private void CreateCells() {
		var wallCount = WallHolder.transform.childCount;
		var walls = new GameObject[ wallCount ];
		Cells = new Cell[ CellsCount ];

		// Get all children
		for ( var i = 0 ; i < wallCount ; i++ ) {
			walls[ i ] = WallHolder.transform.GetChild( i ).gameObject;
		}

		// Assign walls to each cells
		var xProcess = 0;
		var zProcess = 0;
		var lineFeed = 0;

		for ( var cellProcess = 0 ; cellProcess < CellsCount ; cellProcess++ ) {
			Cells[ cellProcess ] = new Cell();
			Debug.Log( "" );

			Cells[ cellProcess ].West = walls[ xProcess ];
			Cells[ cellProcess ].South = walls[ zProcess + MazeZ * ( MazeX + 1 ) ];

			lineFeed++;

			if ( lineFeed == MazeX ) {
				xProcess += 2;
				lineFeed = 0;
			} else {
				xProcess++;
			}

			zProcess++;

			Cells[ cellProcess ].East = walls[ xProcess ];
			Cells[ cellProcess ].North = walls[ zProcess + MazeZ * ( MazeX + 1 ) + MazeX - 1 ];
		}

		CreateMaze();
	}

	#endregion

	private void CreateMaze() {
		FindAvailableNeighboors();
	}

	private void FindAvailableNeighboors() {
		var length = 0;
		var neighboors = new int[ 4 ];
		var check = ( ( ( ( currentCell + 1 ) / MazeX ) - 1 ) * MazeX ) + MazeX;    // currentCell % MazeX

		// Wrong input
		if ( currentCell > CellsCount ) {
			return;
		}

		// North
		if ( currentCell + MazeX < CellsCount ) {
			if ( Cells[ currentCell + MazeX ].Visited == false ) {
				neighboors[ length ] = currentCell + MazeX;
				length++;
			}
		}

		// South
		if ( currentCell - MazeX >= 0 ) {
			if ( Cells[ currentCell - MazeX ].Visited == false ) {
				neighboors[ length ] = currentCell - MazeX;
				length++;
			}
		}

		// West
		if ( currentCell - 1 >= 0 && currentCell != check ) {
			if ( Cells[ currentCell - 1 ].Visited == false ) {
				neighboors[ length ] = currentCell - 1;
				length++;
			}
		}

		// East
		if ( currentCell + 1 < CellsCount && ( currentCell + 1 != check ) ) {
			if ( Cells[ currentCell + 1 ].Visited == false ) {
				neighboors[ length ] = currentCell + 1;
				length++;
			}
		}


	}

}
