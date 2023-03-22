interface ChildProps {
  color: string;
  children?: React.ReactNode;
}

export const Child = ({ color }: ChildProps) => {
  return (
    <div>
      {color}
      <button>Click me</button>
    </div>
  );
};

export const ChildAsFC: React.FC<ChildProps> = ({ color }) => {
  return (
    <div>
      {color}
      <button>Click me</button>
    </div>
  );
};
