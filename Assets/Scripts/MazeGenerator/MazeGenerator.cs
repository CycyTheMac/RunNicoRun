using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	// Inspired from https://www.youtube.com/playlist?list=PL67XFC3MYQ6K7rXSnUpWXV844jdd3X7Lq

	#region SubClasses

	[System.Serializable]
	public class Cell {
		public bool Visited;
		public GameObject North; // 1
		public GameObject South; // 2
		public GameObject West;  // 3
		public GameObject East;  // 4
	}

	#endregion

	#region Inspector's Variables

	[Header( "Maze:" )]
	[SerializeField] private GameObject WallHolder;
	[SerializeField] [Range( 1 , 50 )] private int MazeX = 10;
	[SerializeField] [Range( 1 , 50 )] private int MazeZ = 10;

	[Header( "Walls:" )]
	[SerializeField] private float WallWidth = 0.10f;
	[SerializeField] private float WallLength = 1.00f;
	[SerializeField] private GameObject[] WallPrefabs;

	#endregion

	#region Local Variables

	private Vector3 InitialPosition;
	private bool CreationStarted = false;

	private Cell[] Cells;
	private int CellsCount = 0;
	private int CurrentCell = 0;
	private int VisitedCells = 0;
	private int CurrentNeighboor = 0;

	private List<int> LastCells;
	private int BackingUp = 0;
	private int WallToBreak = 0;


	#endregion


	#region Unity Functions

	private void Start() {
		CellsCount = MazeX * MazeZ;
		CreateWalls();
	}

	private void Update() {

	}

	#endregion

	#region MazeGenerator Functions

	private void CreateWalls() {
		// GameObject temporaryWall;

		// Center on world
		Vector3 myPosition = InitialPosition = new Vector3( ( WallWidth - MazeX ) / 2 , 0 , ( WallLength - MazeZ ) / 2 );


		// X Axis
		for ( var x = 0 ; x < MazeX ; x++ ) {
			for ( var z = 0 ; z <= MazeZ ; z++ ) {
				myPosition = new Vector3( InitialPosition.x + WallLength * ( z - 0.5f ) , 0 , InitialPosition.z + WallLength * ( x - 0.5f ) );
				Instantiate( WallPrefabs[ Random.Range( 0 , WallPrefabs.Length ) ] , myPosition , Quaternion.identity , WallHolder.transform );
			}
		}

		// Z Axis
		for ( var x = 0 ; x <= MazeX ; x++ ) {
			for ( var z = 0 ; z < MazeZ ; z++ ) {
				myPosition = new Vector3( InitialPosition.x + WallLength * z , 0 , InitialPosition.z + WallLength * ( x - 1 ) );
				Instantiate( WallPrefabs[ Random.Range( 0 , WallPrefabs.Length ) ] , myPosition , Quaternion.Euler( 0 , 90 , 0 ) , WallHolder.transform );
			}
		}

		CreateCells();

	}

	private void CreateCells() {
		LastCells = new List<int>();
		LastCells.Clear();
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

	private void FindAvailableNeighboors() {
		var length = 0;
		var neighboors = new int[ 4 ];
		var connectingWall = new int[ 4 ];
		var check = ( ( ( ( CurrentCell + 1 ) / MazeX ) - 1 ) * MazeX ) + MazeX;    // currentCell % MazeX

		// Wrong input
		if ( CurrentCell > CellsCount ) {
			return;
		}

		// North
		if ( CurrentCell + MazeX < CellsCount ) {
			if ( Cells[ CurrentCell + MazeX ].Visited == false ) {
				neighboors[ length ] = CurrentCell + MazeX;
				connectingWall[ length ] = 1;
				length++;
			}
		}

		// South
		if ( CurrentCell - MazeX >= 0 ) {
			if ( Cells[ CurrentCell - MazeX ].Visited == false ) {
				neighboors[ length ] = CurrentCell - MazeX;
				connectingWall[ length ] = 2;
				length++;
			}
		}

		// West
		if ( CurrentCell - 1 >= 0 && CurrentCell != check ) {
			if ( Cells[ CurrentCell - 1 ].Visited == false ) {
				neighboors[ length ] = CurrentCell - 1;
				connectingWall[ length ] = 3;
				length++;
			}
		}

		// East
		if ( CurrentCell + 1 < CellsCount && ( CurrentCell + 1 != check ) ) {
			if ( Cells[ CurrentCell + 1 ].Visited == false ) {
				neighboors[ length ] = CurrentCell + 1;
				connectingWall[ length ] = 4;
				length++;
			}
		}

		if ( length != 0 ) {
			var choosen = Random.Range( 0 , length );
			CurrentNeighboor = neighboors[ choosen ];
			WallToBreak = connectingWall[ choosen ];
		} else {
			if ( BackingUp > 0 ) {
				CurrentCell = LastCells[ BackingUp ];
				BackingUp--;
			}
		}

		// Debug.Log( $"{CurrentCell} ({check}) = {neighboors[ 0 ]}, {neighboors[ 1 ]}, {neighboors[ 2 ]}, {neighboors[ 3 ]}" );

	}

	private void BreakWall() {
		switch ( WallToBreak ) {
			case 1: Destroy( Cells[ CurrentCell ].North ); break;
			case 2: Destroy( Cells[ CurrentCell ].South ); break;
			case 3: Destroy( Cells[ CurrentCell ].West ); break;
			case 4: Destroy( Cells[ CurrentCell ].East ); break;
		}
	}

	private void CreateMaze() {

		while ( VisitedCells < CellsCount ) {
			if ( CreationStarted ) {
				FindAvailableNeighboors();
				if ( Cells[ CurrentNeighboor ].Visited == false && Cells[ CurrentCell ].Visited == true ) {
					BreakWall();
					Cells[ CurrentNeighboor ].Visited = true;
					VisitedCells++;
					LastCells.Add( CurrentCell );
					CurrentCell = CurrentNeighboor;

					if ( LastCells.Count > 0 ) {
						BackingUp = LastCells.Count - 1;
					}

				}
			} else {
				CreationStarted = true;
				CurrentCell = Random.Range( 0 , CellsCount );
				Cells[ CurrentCell ].Visited = true;
				VisitedCells++;
			}
		}

		Debug.Log( "Maze Completed." );

		// Invoke( "CreateMaze" , 0 );
	}

	#endregion

}
