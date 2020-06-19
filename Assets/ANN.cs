using System;
using System.Collections;

public class ANN
{
    public float[][][] weights;
    public int[] size;
    public float bias;

    public ANN(int[] size)
    {
        this.size = size;
        this.weights = new float[size.Length - 1][][];
        for (int k = 0; k < size.Length - 1; k++) this.weights[k] = new float[size[k + 1]][];
        for (int k = 0; k < size.Length - 1; k++) for (int i = 0; i < size[k + 1]; i++) this.weights[k][i] = new float[size[k]];
        this.bias = 1;

        //RandomizeWeights();
        //Calculate(new float[]{ 1, 2, 3, 4 });
    }

    public void RandomizeWeights()
    {
        Random r = new Random();
        for (int k = 0; k < this.weights.Length; k++) for (int i = 0; i < this.weights[k].Length; i++) for (int j = 0; j < this.weights[k][i].Length; j++) this.weights[k][i][j] = (float)Math.Round(r.NextDouble(), 1);
    }

    //this is for the NEAT algorithm (An algorithm that combines ANN and generic algorithms)
    public ANN[] CreateChildren(ANN parent1, ANN parent2, int numberOfChildren, float mutationRate)
    {
        ANN[] children = new ANN[numberOfChildren];
        for (int k = 0; k < numberOfChildren; k++) children[k] = new ANN(parent1.size);

        Random r = new Random();
        for (int c = 0; c < children.Length; c++)
        {
            for (int k = 0; k < parent1.weights.Length; k++)
            {
                for (int i = 0; i < parent1.weights[k].Length; i++)
                {
                    for (int j = 0; j < parent1.weights[k][i].Length; j++)
                    {
                        if (r.NextDouble() > 0.5)
                        {
                            if (r.NextDouble() < mutationRate) children[c].weights[k][i][j] = parent1.weights[k][i][j] + (float)r.NextDouble() - 0.5f;
                            else children[c].weights[k][i][j] = parent1.weights[k][i][j];
                        }
                        else
                        {
                            if (r.NextDouble() < mutationRate) children[c].weights[k][i][j] = parent2.weights[k][i][j] + (float)r.NextDouble() - 0.5f;
                            else children[c].weights[k][i][j] = parent2.weights[k][i][j];
                        }
                    }
                }
            }
            if (r.NextDouble() > 0.5)
            {
                children[c].bias = parent2.bias + (float)r.NextDouble() - 0.5f;
            }
            else children[c].bias = parent2.bias + (float)r.NextDouble() - 0.5f;
        }
        return children;
    }


    public float Sum(float[] l1, float[] l2)
    {
        float sum = 0;
        for (int k = 0; k < l1.Length; k++) sum += l1[k] * l2[k];
        return sum;
    }
    public float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value));
    }
    public float[][] Calculate(float[] input)
    {
        float[][] output = new float[this.size.Length][];
        output[0] = input;
        for (int k = 0; k < this.weights.Length; k++) output[k + 1] = new float[this.weights[k].Length];

        for(int k = 0; k < this.weights.Length; k++)
        {
            for(int i = 0; i < this.weights[k].Length; i++)
            {
                output[k + 1][i] = Sigmoid(Sum(this.weights[k][i], output[k]) + this.bias);
            }
        }

        //return output[output.Length - 1];
        return output;
    }
}






public class ANN2
{
    public int[,] weights;

    public ANN2(int[,] size)
    {
        this.weights = new int[4, 3];

        //RandomizeWeights();
        //debug.log(weights);
        //debug.log(new int[5]);
    }

    /*float[] Calculate()
    {
        return float[3];
    }*/

    //simply randomizes the network's weights
    /*void RandomizeW()
    {
        for(int k = 0; k < weights.Length; k++)
        {
            for (int i = 0; i < weights[k].Length; i++)
            {
                print(k);
                //weights[k][i] = Random.Range(0f, 1f);
            }
        }
    }*/
}

//int[] TestSize = { 4, 3, 2 };
//ANN test = ANN(TestSize);

